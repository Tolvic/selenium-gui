var Demo = (function () {
    // Public functions
    function addEventBindings() {
        setUpCopyJsonEventBindings();
        setUpbuttonToggleBindigs();
    }

    // private functions
    function setUpbuttonToggleBindigs() {
        $(".btn-toggle").click(function() {
            toggleButtonBetweenSecondaryAndSucessClasses($(this));
        });
    }

    function toggleButtonBetweenSecondaryAndSucessClasses(element) {
        element.toggleClass("btn-secondary");
        element.toggleClass("btn-success");

    }

    function setUpCopyJsonEventBindings() {
        $(".copy-json").click(function () {
            var code = $(this).closest($("div.example-sequence")).find("code").text();
            var clipboard = $("#clipboard");
            clipboard.val(code);
            clipboard.show();
            clipboard.select();
            document.execCommand("copy");
            clipboard.hide();
        });
    }

    return {
        addEventBindings: addEventBindings
    }
})();

$(document).ready(function () {
    Demo.addEventBindings();
});