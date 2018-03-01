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

    function loadPartial(url, container, loadingValue = constants.loadingDefault, errorValue = constants.errorDefault) {
        var domContainer = document.getElementById(container);

        domContainer.innerHTML = loadingValue;
        load(url, function (data) { domContainer.innerHTML = data; }, function () { domContainer.innerHTML = errorDefault; })
    }

    function onPageLoad(callback) {
        if (document.readyState !== "loading") {
            callback();
        } else {
            document.addEventListener("DOMContentLoaded", callback);
        }
    }

    return {
        onPageLoad: onPageLoad,
        load: load,
        loadPartial: loadPartial
    }
})();