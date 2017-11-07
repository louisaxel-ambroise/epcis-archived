var fastnt = (function () {
    function load(url, success, error) {
        var request = new XMLHttpRequest();
        request.open('GET', url, true);

        request.onload = function () {
            if (this.status >= 200 && this.status < 400) success(this.response);
            else error();
        };

        request.onerror = error;
        request.send();
    }

    function loadPartial(url, container) {
        load(url, function (data) { document.getElementById(container).innerHTML = data; }, function () { document.getElementById(container).innerHTML = "<p>Error...</p>"; })
    }

    return {
        load: load,
        loadPartial: loadPartial
    }
})();