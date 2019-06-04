var sequence = (function () {
    // Public functions
    function run() {
        $("#run-sequence").click(function() {
            var sequenceData = getSequenceData();
            sendRunSequence(sequenceData);
        });
    }

    function addStep() {
        $("#add-step").click(function() {
            console.log("Add Step function ran");
            var stepTemplate = getStepTemplate();
            console.log(stepTemplate);
        });
    }

    // private functions
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

        step.find(".step-input").each(function () {
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
        var stepTemplate;

        $.ajax({
            type: "GET",
            url: "/Sequence/GetStepTemplate",
            contentType: 'application/json',
            dataType: "json",
            success: function(response) {
                stepTemplate = response;
            }
        });

        return stepTemplate;
    }

    // Expose functions
    return {
        run: run,
        addStep: addStep
    }
})();

$(document).ready(function () {
    sequence.run();
    sequence.addStep();
});



