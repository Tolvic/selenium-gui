﻿/// <reference path="../../selenium-gui/wwwroot/lib/jquery/dist/jquery.min.js"/>
/// <reference path="../../selenium-gui/wwwroot/js/Sequence.js"/>
/// <reference path="../Jasmine/lib/jasmine-3.4.0/jasmine.js"/>
/// <reference path="../Jasmine/lib/jasmine-3.4.0/jasmine-html.js"/>
/// <reference path="../Jasmine/lib/jasmine-3.4.0/jasmine-ajax.js"/>

describe("Sequence.js", function () {

    beforeEach(function () {
        jasmine.Ajax.install();
        var mockHtmlContainer = '<div id="mock-html-container">';
        var mockHtml = '<form>' +
            '<ul id = "sequence-list" class="list-group list-group-flush" >' +
            '<li class="list-group-item step-li">' +
            '<div class="row step">' +
            '<div class="col-auto">' +
            '<select class="form-control step-type">' +
            '<option>Alert Operations</option>' +
            '<option>Browser Operations</option>' +
            '<option>Cookie Operations</option>' +
            '<option>Element Operations</option>' +
            '<option>Find Element</option>' +
            '<option selected>Navigate</option>' +
            '<option>Select And Deselect</option>' +
            '<option>Wait</option>' +
            '<option>Wait For</option>' +
            '</select>' +
            '</div>' +
            '<div class="col">' +
            '<div id="alert-operations-inputs" class="row step-inputs" style="display: none;">' +
            '<select class="form-control step-input primary-input step-parameter-0 col">' +
            '<option>Accept</option>' +
            '<option>Dismiss</option>' +
            '<option data-secondary-input="true">Send Keys</option>' +
            '</select>' +
            '<input type="text" class="form-control step-input secondary-input step-parameter-1 col-9 ml-sm-2" name="select" placeholder="Text" style="display: none;">' +
            '</div>' +
            '<div id="browser-operations-inputs" class="row step-inputs" style="display: none;">' +
            '<select class="form-control step-input primary-input step-parameter-0 col">' +
            '<option selected>Maximize</option>' +
            '<option>Quit</option>' +
            '</select>' +
            '</div>' +
            '<div id="cookie-operations-inputs" class="row step-inputs" style="display: none;">' +
            '<select class="form-control step-input primary-input secondary-options step-parameter-0 col">' +
            '<option data-secondary-input="true" data-tertiary-input="true">Add Cookie</option>' +
            '<option Selected>Delete All Cookies</option>' +
            '<option data-secondary-input="true">Delete Cookie By Name</option>' +
            '</select>' +
            '<div class="step-input secondary-input col-9 ml-sm-2" style="display: none">' +
            '<div class="row">' +
            '<input type="text" class="form-control step-input step-parameter-1 col" name="cookie-name" placeholder="Name">' +
            '<input type="text" class="form-control step-input step-parameter-2 tertiary-input col ml-sm-2" name="selector" placeholder="Value" style="display: none;">' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div id="element-operations-inputs" class="row step-inputs" style="display: none;">' +
            '<select class="form-control step-input primary-input step-parameter-0 col">' +
            '<option selected>Click</option>' +
            '<option>Clear</option>' +
            '<option data-secondary-input="true">Send Keys</option>' +
            '<option>Submit</option>' +
            '<option>Scroll To Element</option>' +
            '</select>' +
            '<input type="text" class="form-control step-input secondary-input step-parameter-0 col-9 ml-sm-2" name="keys" placeholder="Text" style="display: none;">' +
            '</div>' +
            '<div id="find-element-inputs" class="row step-inputs" style="display: none;">' +
            '<select class="form-control step-input primary-input step-parameter-0 col">' +
            '<option data-secondary-input="true">By Class Name</option>' +
            '<option data-secondary-input="true">By CSS Selector</option>' +
            '<option data-secondary-input="true" selected>By ID</option>' +
            '<option data-secondary-input="true">By Link Text</option>' +
            '<option data-secondary-input="true">By Name</option>' +
            '<option data-secondary-input="true">By Partial Link Text</option>' +
            '<option data-secondary-input="true">By Tag Name</option>' +
            '<option data-secondary-input="true">By XPath</option>' +
            '</select>' +
            '<input type="text" class="form-control step-input step-parameter-1 secondary-input col-9 ml-sm-2" name="selector" placeholder="Selector">' +
            '</div>' +
            '<div id="navigate-inputs" class="row step-inputs">' +
            '<select class="form-control step-input step-parameter-0 primary-input col">' +
            '<option>Back</option>' +
            '<option>Forward</option>' +
            '<option data-secondary-input="true" selected>Go To URL</option>' +
            '<option>Refresh</option>' +
            '</select>' +
            '<input type="url" class="form-control step-input step-parameter-1 secondary-input col-9 ml-sm-2" name="url" placeholder="URL">' +
            '</div>' +
            '<div id="select-and-deselect-inputs" class="row step-inputs" style="display: none;">' +
            '<select class="form-control step-input step-parameter-0 primary-input col">' +
            '<option data-secondary-input="true">Select By Index</option>' +
            '<option data-secondary-input="true" selected>Select By Text</option>' +
            '<option data-secondary-input="true">Select By Value</option>' +
            '<option>Deselect All</option>' +
            '<option data-secondary-input="true">Deselect By Index</option>' +
            '<option data-secondary-input="true">Deselect By Text</option>' +
            '<option data-secondary-input="true">Deselect By Value</option>' +
            '</select>' +
            '<input type="text" class="form-control step-input step-parameter-1 secondary-input col-9 ml-sm-2" name="select" placeholder="Selector">' +
            '</div>' +
            '<div id="wait-inputs" class="row step-inputs" style="display: none;">' +
            '<input type="number" class="form-control step-input step-parameter-0 col-9" name="wait" placeholder="number">' +
            '<select class="form-control step-input step-parameter-1 col ml-sm-2">' +
            '<option selected>Seconds</option>' +
            '<option>Miliseconds</option>' +
            '</select>' +
            '</div>' +
            '<div id="wait-for-inputs" class="row step-inputs" style="display: none;">' +
            '<select class="form-control step-input primary-input step-parameter-0 secondary-options col">' +
            '<option>Alert To Be Present</option>' +
            '<option data-secondary-input="true">Element Text To Be</option>' +
            '<option>Element To Be Clickable</option>' +
            '<option>Element To Be Selected</option>' +
            '<option selected>Element To Be Visible</option>' +
            '<option data-secondary-input="true" data-tertiary-input="true">Element Text To Be Present</option>' +
            '<option data-secondary-input="true" data-tertiary-input="true">Element To Exist</option>' +
            '</select>' +
            '<div class="step-input secondary-input col-9 ml-sm-2">' +
            '<div class="row">' +
            '<select class="form-control step-input tertiary-input step-parameter-1 col-3 mr-sm-2" style="display:none;">' +
            '<option>By Class Name</option>' +
            '<option>By CSS Selector</option>' +
            '<option selected>By ID</option>' +
            '<option>By Link Text</option>' +
            '<option>By Name</option>' +
            '<option>By Partial Link Text</option>' +
            '<option>By Tag Name</option>' +
            '<option>By XPath</option>' +
            '</select>' +
            '<input type="text" class="form-control step-parameter-2 step-input col" name="selector" placeholder="Selector">' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="col-1">' +
            '<button type="button" class="btn btn-link delete-step"><i class="fas fa-times fa-lg"></i></button>' +
            '</div>' +
            '</div>' +
            '</li>' +
            '</ul>' +
            '<div class="card-footer">' +
            '<div class="row">' +
            '<div class="col">' +
            '<button type="button" id="run-sequence" class="btn btn-success">Run Sequence</button>' +
            '</div>' +
            '<div class="col">' +
            '<button type="button" id="add-step" class="btn btn-primary">Add Step</button>' +
            '</div>' +
            '<div class="col">' +
            '<button class="btn btn-info">Get Script</button>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</form >';
        var htmlToTest = mockHtmlContainer + mockHtml + '</div>';
        $(htmlToTest).appendTo('body');
    });

    afterEach(function () {
        $("#mock-html-container").remove();
        jasmine.Ajax.uninstall();
    });

    function setup() {
    
    }

    it("Clicking Run Sequences Makes An AJAX Request To Sequence Run Passing Form Data", function() {
        // Arrange
        var sequencetest = sequence;
        sequencetest.addEventBindings();

        // Act
        $(".step-parameter-1").val("https://google.com");
        $("#run-sequence").click();
        var request = jasmine.Ajax.requests.mostRecent();

        // Assert
        expect(request.url).toBe('/Sequence/Run?sequenceData=%5B%22%7B%5C%22Type%5C%22%3A%5C%22Navigate%5C%22%2C%5C%22Parameters%5C%22%3A%5B%5C%22Go%20To%20URL%5C%22%2C%5C%22https%3A%2F%2Fgoogle.com%5C%22%5D%7D%22%5D');
    });

    it("Clicking Add Steps Makes An AJAX Request To Sequence GetStepTemplate", function () {
        // Arrange
        var sequencetest = sequence;
        sequencetest.addEventBindings();

        // Act
        $("#add-step").click();
        var request = jasmine.Ajax.requests.mostRecent();

        // Assert
        expect(request.url).toBe('/Sequence/GetStepTemplate');
    });

    it("Clicking Delete deletes step", function () {
        // Arrange
        var sequencetest = sequence;
        sequencetest.addEventBindings();

        // Act
        $(".delete-step").click();   
        var numberOfSteps = $(".step-li").length;

        // Assert
        expect(numberOfSteps).toBe(0);
    });
});