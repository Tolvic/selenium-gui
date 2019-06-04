var sequence = (function () {
    // Public functions
    function addEventBindings() {
        setUpRunEventBinding();
        setUpAddStepEventBinding();
        setUpDeleteStepEventBinding();
    }

   // private functions
    function setUpRunEventBinding() {
        $("#run-sequence").click(function () {
            var sequenceData = getSequenceData();
            sendRunSequence(sequenceData);
        });
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
    }

    // Expose functions
    return {
        addEventBindings: addEventBindings
    }
})();

$(document).ready(function () {
    sequence.addEventBindings();
});



