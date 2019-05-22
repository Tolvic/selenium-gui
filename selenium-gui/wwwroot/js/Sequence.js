$(document).ready(function () {
    RunSequence();
});

function GetSequenceData() {
    var sequenceData = [];
    $(".step").each(function () {
        var type = getStepType($(this));
        var options = getStepOptions($(this));
        var stepObject = stepObjectInit(type, options);
        sequenceData.push(JSON.stringify(stepObject));
    });
    console.log(sequenceData);
    return JSON.stringify(sequenceData);
}

function RunSequence() {
    $("#run-sequence").click(function () {
        var sequenceData = GetSequenceData();
        SendRunSequence(sequenceData);
    });
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

function SendRunSequence(sequenceData) {
    $.ajax({
        type: "GET",
        url: "/Sequence/Run",
        data: {
            sequenceData: sequenceData
        },
        contentType: 'application/json',
        dataType: "json",
        success: function (response) {
            alert("Finsihed");
        }
    });
}