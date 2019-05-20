// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    RunSequence();
});

function GetSequenceData() {
    var sequenceData = new Array();
    i = 0
    $(".step").each(function () {
        var stepType = $(this).contents().find(".step-type").val();

        var stepOptions = new Array();

        sequenceData[i] = new Array();

        sequenceData[i][stepType] = stepOptions;

        i++;
    });

    return sequenceData;
}

function RunSequence() {
    $("#run-sequence").click(function() {
        GetSequenceData();
        SendRunSequence();
    });
}

function SendRunSequence() {
    $.ajax({
        type: "GET",
        url: "/Sequence/Run",
        data: {
            goToUrl: ""
        },
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            alert("Finsihed");
        }
    });
}