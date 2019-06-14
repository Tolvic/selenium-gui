var Demo = (function () {
    // Public functions
    function setUpCopyJsonEventBindings() {
        
    }

    // private functions
    function setUpCopyJsonEventBindings() {
        $(".copy-json").click(function () {
            var code = $(this).closest($("div.example")).find("code").text();
            var clipboard = $("#clipboard");
            clipboard.val(code);
            clipboard.show();
            clipboard.select();
            document.execCommand("copy");
            clipboard.hide();
        });
    }

    return {
        setUpCopyJsonEventBindings: setUpCopyJsonEventBindings
    }
})();

$(document).ready(function () {
    Demo.setUpCopyJsonEventBindings();
});