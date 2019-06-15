/// <reference path="../../selenium-gui/wwwroot/lib/jquery/dist/jquery.min.js"/>
/// <reference path="../../selenium-gui/wwwroot/js/Demo.js"/>
/// <reference path="../Jasmine/lib/jasmine-3.4.0/jasmine.js"/>
/// <reference path="../Jasmine/lib/jasmine-3.4.0/jasmine-html.js"/>
/// <reference path="../Jasmine/lib/jasmine-3.4.0/jasmine-ajax.js"/>

describe("Demo.js", function () {
    var mockHtmlContainer = '<div id="mock-html-container">';
    var mockHtml = ''

    beforeEach(function () {
        jasmine.Ajax.install();
        var htmlToTest = mockHtmlContainer + mockHtml + '</div>';
        $(htmlToTest).appendTo('body');
    });

    afterEach(function () {
        //$("#mock-html-container").remove();
        jasmine.Ajax.uninstall();
    });

    function setup() {

    }

    it("Clicking Copy JSON to clipboard sets textarea value to JSON code", function () {
        // Arrange
        spyOn(document, 'execCommand');

        // Act
        var html = '<section id="find-and-click">' +
            '<div class="row example-sequence">' +
            '<div class="col-3">' +
            '<button id="find-element-by-id-button" class="btn btn-secondary">Find me by ID</button>' +
            '</div>' +
            '<div class="col">' +
            '<div class="text-center">' +
            '<button class="btn btn-info" type="button" data-toggle="collapse" data-target="#find-and-click-code" aria-expanded="false" aria-controls="find-and-click-code">See Sequence JSON</button>' +
            '</div>' +
            '<div class="collapse" id="find-and-click-code">' +
            '<div class="card card-body">' +
            '<pre>' +
            '<code find-element-by-id-button-code>test script</code>' +
            '</pre>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="col-3 text-right">' +
            '<button class="btn btn-primary copy-json">Copy JSON to clipboard</button>' +
            '</div>' +
            '</div>' +
            '</section>' +
            '<textarea id="clipboard" style="display: none;"></textarea>';
        $("#mock-html-container").html(html);
        Demo.addEventBindings();
        $(".copy-json").click();
        var textArea = $("#clipboard").val();


        // Assert
        expect(textArea).toBe("test script");
    });

    it("Clicking Copy JSON to clipboard calls document.execCommand with copy", function () {
        // Arrange
        spyOn(document, 'execCommand').and.callThrough();

        // Act
        var html = '<section id="find-and-click">' +
            '<div class="row example-sequence">' +
            '<div class="col-3">' +
            '<button id="find-element-by-id-button" class="btn btn-secondary">Find me by ID</button>' +
            '</div>' +
            '<div class="col">' +
            '<div class="text-center">' +
            '<button class="btn btn-info" type="button" data-toggle="collapse" data-target="#find-and-click-code" aria-expanded="false" aria-controls="find-and-click-code">See Sequence JSON</button>' +
            '</div>' +
            '<div class="collapse" id="find-and-click-code">' +
            '<div class="card card-body">' +
            '<pre>' +
            '<code find-element-by-id-button-code>test script</code>' +
            '</pre>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="col-3 text-right">' +
            '<button class="btn btn-primary copy-json">Copy JSON to clipboard</button>' +
            '</div>' +
            '</div>' +
            '</section>' +
            '<textarea id="clipboard" style="display: none;"></textarea>';
        $("#mock-html-container").html(html);
        Demo.addEventBindings();
        $(".copy-json").click();


        // Assert
        expect(document.execCommand).toHaveBeenCalledWith('copy');
    });
});