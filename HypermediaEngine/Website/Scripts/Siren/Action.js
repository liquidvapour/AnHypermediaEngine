function SirenAction(sirenAction, entityAddress) {
    this.subscriptions = [];
    this.error = ko.observable();
    
    this.entityAddress = ko.observable(entityAddress);

    this.fields = ko.observableArray();

    this.name = ko.observable();
    this.actionLinkName = ko.observable();

    ko.mapping.fromJS(sirenAction, SirenMappings.prototype.ActionMapping, this);

    this.errors = ko.validation.group([this.fields], { deep: true, observable: false });

    if (this.name() === "update") {
        this.actionLinkName("Edit");
    }
    else {
        this.actionLinkName(this.name());
    }

    return this;
};

SirenAction.prototype.isValid = function () {
    if (this.errors().length > 0) {
        this.errors.showAllMessages(true);
        return false;
    }

    return true;
};

SirenAction.prototype.execute = function () {
    this.error(undefined);
    
    if (this.isValid() === false) {
        return false;
    }

    if (this.method() === "GET") {
        this.get(this.href(), this.getQueryPayload());
    }
    else if (this.method() === "POST") {
        this.post(this.href(), this.getCommandPayload());
    }
    else if (this.method() === "PUT") {
        this.put(this.href(), this.getCommandPayload());
    }
    else if (this.method() === "DELETE") {
        this.delete(this.href());
    }
};


SirenAction.prototype.get = function (href, parameters) {
    $.ajax({
        type: "GET",
        url: href + parameters,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", this.getCookie("accessToken"));
        }.bind(this),
        success: this.handleSuccess.bind(this),
        error: this.handleError.bind(this),
        dataType: "application/vnd.siren+json"
    });
};

SirenAction.prototype.getQueryPayload = function () {
    var parameters = "?";

    for (var i = 0; i < this.fields().length; i++) {
        if (this.fields()[i].value() != undefined) {
            if (parameters !== "?") {
                parameters += "&";
            }

            parameters += this.fields()[i].name() + "=" + this.fields()[i].value();
        }
    }

    return parameters;
};


SirenAction.prototype.post = function (href, payload) {
    $.ajax({
        type: "POST",
        url: href,
        data: payload,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", this.getCookie("accessToken"));
        }.bind(this),
        success: this.handleSuccess.bind(this),
        error: this.handleError.bind(this),
        contentType: "application/x-www-form-urlencoded",
        dataType: "json"
    });
};

SirenAction.prototype.put = function (href, payload) {
    $.ajax({
        type: "PUT",
        url: href,
        data: payload,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", this.getCookie("accessToken"));
        }.bind(this),
        success: this.handleSuccess.bind(this),
        error: this.handleError.bind(this),
        contentType: "application/x-www-form-urlencoded",
        dataType: "json"
    });
};

SirenAction.prototype.delete = function (href) {
    $.ajax({
        type: "DELETE",
        url: href,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", this.getCookie("accessToken"));
        }.bind(this),
        success: this.handleSuccess.bind(this),
        error: this.handleError.bind(this),
        contentType: "application/x-www-form-urlencoded",
        dataType: "json"
    });
};

SirenAction.prototype.getCommandPayload = function () {
    var parameters = "";

    for (var i = 0; i < this.fields().length; i++) {
        if (this.fields()[i].value() != undefined) {
            if (parameters !== "") {
                parameters += "&";
            }

            parameters += this.fields()[i].name() + "=" + this.fields()[i].value();
        }
    }

    return parameters;
};


SirenAction.prototype.handleSuccess = function (data) {
    if (_.contains(data.class, "error")) {
        this.error(data.properties["error Message"]);
    }
    else {
        postbox.notifySubscribers(data, this.entityAddress());
    }
};

SirenAction.prototype.handleError = function (jqXHR, textStatus, errorThrown) {
    postbox.notifySubscribers(JSON.parse(jqXHR.responseText), this.entityAddress());
};


SirenAction.prototype.getCookie = function(cookieName) {
    var name = cookieName + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
};


SirenAction.prototype.dispose = function () {
    ko.utils.arrayForEach(this.subscriptions, this.disposeEach);
    ko.utils.objectForEach(this, this.disposeEach);
};

SirenAction.prototype.disposeEach = function (propOrValue, value) {
    var disposable = value || propOrValue;

    if (disposable && typeof disposable.dispose === "function") {
        disposable.dispose();
    }
};