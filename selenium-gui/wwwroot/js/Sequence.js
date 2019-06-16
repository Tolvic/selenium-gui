var sequence = (function () {
    // Public functions
    function addEventBindings() {
        setUpRunSequenceEventBinding();
        setUpAddStepEventBinding();
        setUpDeleteStepEventBinding();
        setUpStepTypeSelectionBinding();
        setUpStepInputBinding();
        setUpSecondaryOptionsBindings();
        setUpExportBindings();
        setUpImportBindings();
        setUpToggleImportButtonBindings();
    }

   // private functions
    function setUpImportBindings() {
        $("#import-sequence-json").click(function () {
            importSequence();
        });
        $("#import-sequence-json-append").click(function () {
            appendSequence();
        });

    }

    function setUpToggleImportButtonBindings() {
        $("#import-text").on('change keyup paste', function () {
            var input = $("#import-text").val();
            if (isJsonString(input)) {
                $("#import-sequence-json").prop('disabled', false);
                $("#import-sequence-json-append").prop('disabled', false);
            } else {
                $("#import-sequence-json").prop('disabled', true);
                $("#import-sequence-json-append").prop('disabled', true);
            }
        });
    }

    function isJsonString(input) {
        try {
            JSON.parse(input);
        } catch (e) {
            return false;
        }
        return true;
    }

    function importSequence() {
        var sequence = getImportSequence();
        clearAllSteps();
        populateSequence(sequence);
        $('#import-modal').modal('toggle');
    }

    function appendSequence() {
        var sequence = getImportSequence();
        populateSequence(sequence);
        $('#import-modal').modal('toggle');
    }

    function getImportSequence() {
        var importText = $("#import-text");
        var sequence = importText.val();
        importText.val("");

        return JSON.parse(sequence);
    }

    function populateSequence(sequence) {
        var i = 0;
        sequence.forEach(function () {
            getStepTemplateWithPromise().then(function () {
                var importStep = sequence[i];
                var stepInputsId = importStep.Type.toLowerCase().replace(/ /g, "-");
                var steps = $(".step-li");
                var step = steps.last();
                var x = 0;

                step.find($(".step-type")).val(importStep.Type).change();
                importStep.Parameters.forEach(function (paramater) {
                    step.find("#" + stepInputsId + "-inputs").find($(".step-parameter-" + x)).val(paramater);
                    x++;
                });
                i++;
            });

        });
    }

    function getStepTemplateWithPromise() {
        return $.ajax({
            type: "GET",
            url: "/Sequence/GetStepTemplate",
            contentType: 'application/json',
            success: function (response) {
                addStepToSequence(response);
            }
        });
    }

    function clearAllSteps() {
        $(".step-li").remove();
    }


    function setUpExportBindings() {
        $("#export-sequence").click(function() {
            addSequenceToExportModal();
        });
        $("#export-copy-to-clipboard").click(function() {
            copyExportSequenceToClipboard();
        });
    }

    function addSequenceToExportModal() {
        var sequenceData = getSequenceData();
        $("#export-text").val(JSON.stringify(sequenceData, null, '\t'));
    }

    function copyExportSequenceToClipboard() {
        $("#export-text").select();
        document.execCommand("copy");
    };

    function setUpRunSequenceEventBinding() {
        $("#run-sequence").click(function () {
            var sequenceData = getSequenceData();
            var jsonSequenceData = stringifySequenceData(sequenceData);
            sendRunSequence(jsonSequenceData);
        });
    }

    function stringifySequenceData(sequence) {
        var sequenceData = [];
        sequence.forEach(function (step) {
            sequenceData.push(JSON.stringify(step));
        });

        return JSON.stringify(sequenceData);
    }

    function setUpStepTypeSelectionBinding() {
        $(".step-type").change(function () {
            toggleStepInputs($(this));
        });
    }

    function toggleStepInputs(stepType) {
        hideAllStepInputs($(stepType));
        showSelectedStepInputs($(stepType));
    }

    function hideAllStepInputs(stepType) {
        var stepInputs = $(stepType).closest("li").find(".step-inputs");
            $(stepType).closest("li").find(stepInputs).hide();
    }

    function showSelectedStepInputs(stepType) {
        var stepInputsId = $(stepType).val().toLowerCase().replace(/ /g, "-");
        $(stepType).closest("li").find("#" + stepInputsId + "-inputs").show();
    }

    function setUpStepInputBinding() {
        $(".primary-input").change(function() {
            toggleSecondaryInputs($(this));
        });
    }

    function toggleSecondaryInputs(primaryInputs) {
        var secondaryInputs = $(primaryInputs).closest("div.step-inputs").find(".secondary-input");
        secondaryInputs.hide();
        showSecondaryInputs(primaryInputs, secondaryInputs);
    }

    function showSecondaryInputs(primaryInputs, secondaryInputs) {
        var displaySecondaryInputFlag = $("option:selected", primaryInputs).data("secondary-input");
        if (displaySecondaryInputFlag) {
            secondaryInputs.show();
        }
    }

    function setUpSecondaryOptionsBindings() {
        $(".secondary-options").change(function() {
            toggleTertiaryInputs($(this));
        });
    }

    function toggleTertiaryInputs(secondaryInputs) {
        var tertiaryInputs = $(secondaryInputs).closest("div.step-inputs").find(".tertiary-input");
        tertiaryInputs.hide();
        showTertiaryInputs(secondaryInputs, tertiaryInputs);
    }

    function showTertiaryInputs(secondaryInputs, tertiaryInputs) {
        var displayTertiaryInputFlag = $("option:selected", secondaryInputs).data("tertiary-input");
        if (displayTertiaryInputFlag) {
            tertiaryInputs.show();
        }
    }

    function setUpAddStepEventBinding() {
        $("#add-step").click(function () {
            getStepTemplate();
        });
    }

    function setUpDeleteStepEventBinding() {
        $(".delete-step").click(function () {
            $(this).closest("li").remove();
        });
    }

    function getSequenceData() {
        var sequenceData = [];
        $(".step").each(function () {
            var type = getStepType($(this));
            var options = getStepOptions($(this));
            var stepObject = stepObjectInit(type, options);
            sequenceData.push(stepObject);
        });
        return sequenceData;
    }

    function getStepType(step) {
        return step.contents().find(".step-type").val();
    }

    function getStepOptions(step) {
        var options = [];

        step.find(".step-inputs:visible").find(".step-input").each(function () {
            options.push(($(this).val()));
        });
        return options;
    }

    function stepObjectInit(type, parameters) {
        return { Type: type, Parameters: parameters }
    }

    function sendRunSequence(sequenceData) {
        $.ajax({
            type: "GET",
            url: "/Sequence/Run",
            data: {
                sequenceData: sequenceData
            },
            contentType: 'application/json',
            dataType: "text",
            success: function (response) {
                displayResponseMessage(response);
            }
        });
    }

    function displayResponseMessage(response) {
        $(".alert").text(response);
        if (response !== "Sequence ran sucessfully") {
            $(".alert").removeClass("alert-success");
            $(".alert").addClass("alert-danger");
        } else {
            $(".alert").removeClass("alert-danger");
            $(".alert").addClass("alert-success");
        }

        $(".alert").removeClass("invisible");
    }

    function getStepTemplate() {
         $.ajax({
            type: "GET",
            url: "/Sequence/GetStepTemplate",
            contentType: 'application/json',
            success: function(response) {
                addStepToSequence(response);
            }
        });
    }

    function addStepToSequence(stepTemplate) {
        $("#sequence-list").append(stepTemplate);
        setUpDeleteStepEventBinding();
        setUpStepTypeSelectionBinding();
        setUpStepInputBinding();
        setUpSecondaryOptionsBindings();
    }

    // Expose functions
    return {
        addEventBindings: addEventBindings
    }
})();

$(document).ready(function () {
    sequence.addEventBindings();
});