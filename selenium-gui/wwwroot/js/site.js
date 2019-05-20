// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    RunSequence();
});

function GetSequenceData() {
    var sequenceData = [];
   $(".step").each(function () {
        var type = $(this).contents().find(".step-type").val();
        var options = [];

        $(this).find(".step-input").each(function() {
            options.push(($(this).val()));
        });

        var stepObject = { stepType: type, stepOpetions: options }

        sequenceData.push(stepObject);
    });

    return JSON.stringify(sequenceData);
}

function RunSequence() {
    $("#run-sequence").click(function() {
        var sequenceData = GetSequenceData();
        SendRunSequence(sequenceData);
    });
}

function SendRunSequence(sequenceData) {
    $.ajax({
        type: "GET",
        url: "/Sequence/Run",
        data: {
            sequenceData: sequenceData
        },
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            alert("Finsihed");
        }
    });
}