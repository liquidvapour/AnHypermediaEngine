function SirenLink(postbox, link) {
    this.postbox = postbox;

    this.title = link.title;
    this.href = ko.observable();
    this.rel = link.rel;

    this.load(link);

    return this;
};

SirenLink.prototype.load = function (link) {
    this.href(link.href);
};

SirenLink.prototype.execute = function (callback) {
    this.get(this.href(), callback);
};

SirenLink.prototype.get = function (href, callback) {
    if (typeof(callback) == "function") {
        $.ajax({
            type: "GET",
            url: href,
            success: callback,
            error: this.handleError.bind(this),
            dataType: "json"
        });
    }
    else {
        $.ajax({
            type: "GET",
            url: href,
            success: this.handleSuccess.bind(this),
            error: this.handleError.bind(this),
            dataType: "json"
        });
    }
};

SirenLink.prototype.handleSuccess = function (response) {
    this.postbox.notifySubscribers(response, "refresh");
};

SirenLink.prototype.handleError = function (jqXHR, textStatus, errorThrown) {
    alert("There was an error processing your request: \n\n" + jqXHR.responseText);
};