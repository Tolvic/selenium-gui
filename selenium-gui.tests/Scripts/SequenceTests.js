/// <reference path="../../selenium-gui/wwwroot/lib/jquery/dist/jquery.min.js"/>
/// <reference path="../../selenium-gui/wwwroot/js/Sequence.js"/>
/// <reference path="../Jasmine/lib/jasmine-3.4.0/jasmine.js"/>
/// <reference path="../Jasmine/lib/jasmine-3.4.0/jasmine-html.js"/>
/// <reference path="../Jasmine/lib/jasmine-3.4.0/jasmine-ajax.js"/>

describe("Sequence.js", function () {
    var mockHtmlContainer = '<div id="mock-html-container">';
    var mockHtml = '<form>' +
        '<ul id = "sequence-list" class="list-group list-group-flush" >' +
        '<li class="list-group-item step-li">' +
        '<div class="row step">' +
        '<div class="col-3">' +
        '<select class="form-control step-type">' +
        '<option selected="">Go To URL</option>' +
        '<option>...</option>' +
        '</select>' +
        '</div>' +
        '<div class="col">' +
        '<input id="test-input" type="url" class="form-control step-input" name="url" placeholder="URL">' +
        '</div>' +
        '<div class="col-1">' +
        '<button type="button" id="test-delete" class="btn btn-link delete-step"><i class="fas fa-times fa-lg"></i></button>' +
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

    beforeEach(function () {
        jasmine.Ajax.install();
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
        $("#test-input").val("https://google.com");
        $("#run-sequence").click();
        var request = jasmine.Ajax.requests.mostRecent();

        // Assert
        expect(request.url).toBe('/Sequence/Run?sequenceData=%5B%22%7B%5C%22Type%5C%22%3A%5C%22Go%20To%20URL%5C%22%2C%5C%22Parameters%5C%22%3A%5B%5C%22https%3A%2F%2Fgoogle.com%5C%22%5D%7D%22%5D');
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
        $("#test-delete").click();   
        var numberOfSteps = $(".step-li").length;

        // Assert
        expect(numberOfSteps).toBe(0);
    });
});