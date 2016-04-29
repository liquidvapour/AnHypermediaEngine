function SirenLink(sirenLink, entityAddress) {
    this.entityAddress = ko.observable(entityAddress);

    ko.mapping.fromJS(sirenLink, null, this);

    return this;
};

SirenLink.prototype.execute = function (callback) {
    this.get(this.href(), callback);
};

SirenLink.prototype.get = function (href, callback) {
    if (typeof(callback) == "function") {
        $.ajax({
            type: "GET",
            url: href,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", this.getCookie("accessToken"));
            }.bind(this),
            success: callback,
            error: this.handleError.bind(this),
            dataType: "json"
        });
    }
    else {
        $.ajax({
            type: "GET",
            url: href,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", this.getCookie("accessToken"));
            }.bind(this),
            success: this.handleSuccess.bind(this),
            error: this.handleError.bind(this),
            dataType: "json"
        });
    }
};

SirenLink.prototype.handleSuccess = function (data) {
    postbox.notifySubscribers(data, this.entityAddress());
};

SirenLink.prototype.handleError = function (jqXHR, textStatus, errorThrown) {
    postbox.notifySubscribers(JSON.parse(jqXHR.responseText), this.entityAddress());
};


SirenLink.prototype.getCookie = function(cookieName) {
    var name = cookieName + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
};