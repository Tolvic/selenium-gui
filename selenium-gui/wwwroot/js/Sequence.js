var sequence = (function () {
    // Public functions
    function addEventBindings() {
        setUpRunSequenceEventBinding();
        setUpAddStepEventBinding();
        setUpDeleteStepEventBinding();
        setUpStepTypeSelectionBinding();
        setUpStepInputBinding();
    }

   // private functions
    function setUpRunSequenceEventBinding() {
        $("#run-sequence").click(function () {
            var sequenceData = getSequenceData();
            sendRunSequence(sequenceData);
        });
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
            sequenceData.push(JSON.stringify(stepObject));
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
                sequenceData: JSON.stringify(sequenceData)
            },
            contentType: 'application/json',
            dataType: "json",
            success: function (response) {
                alert("Finsihed");
            }
        });
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
    }

    // Expose functions
    return {
        addEventBindings: addEventBindings
    }
})();

$(document).ready(function () {
    sequence.addEventBindings();
});



