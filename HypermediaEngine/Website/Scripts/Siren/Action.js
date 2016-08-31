function SirenAction(postbox, action) {
    this.postbox = postbox;

    this.title = action.title;
    this.name = action.name;

    this.method = action.method;
    this.href = ko.observable();

    this.type = action.type;
    this.fields = ko.observableArray();

    this.serverError = ko.observable();
    this.errors = ko.observableArray();
    this.load(action);

    return this;
};

SirenAction.prototype.load = function (action) {
    this.href(action.href);

    if (action.fields) {
        this.fields.remove(function (field) { return !_.any(action.fields, function (responseField) { return field.name === responseField.name; }); });
        for (var fieldIndex = 0; fieldIndex < action.fields.length; fieldIndex++) {
            var currentField = _.filter(this.fields(), function (field) { return field.name === action.fields[fieldIndex].name; })[0];

            if (currentField) {
                currentField.load(action.fields[fieldIndex]);
            } else {
                this.fields.push(new SirenField(action.fields[fieldIndex]));
            }
        }

        this.errors = ko.validation.group([this.fields], { deep: true, observable: false });
    }
};

SirenAction.prototype.isValid = function () {
    this.serverError(null);

    if (this.errors().length > 0) {
        this.errors.showAllMessages(true);
        return false;
    }

    return true;
};

SirenAction.prototype.execute = function () {
    if (this.isValid() === false) {
        return false;
    }

    if (this.method === "GET") {
        this.get(this.href(), this.getQueryPayload());
    }
    else if (this.method === "POST") {
        this.post(this.href(), this.getCommandPayload());
    }
    else if (this.method === "PUT") {
        this.put(this.href(), this.getCommandPayload());
    }
    else if (this.method === "PATCH") {
        this.patch(this.href(), this.getCommandPayload());
    }
    else if (this.method === "DELETE") {
        this.delete(this.href());
    }
};


SirenAction.prototype.get = function (href, parameters) {
    if (_.contains(href, "?")) {
        parameters = parameters.replace("?", "&");
    }

    $.ajax({
        type: "GET",
        url: href + parameters,
        success: this.handleSuccess.bind(this),
        error: this.handleError.bind(this)
    });
};

SirenAction.prototype.getQueryPayload = function () {
    var parameters = "?";

    for (var i = 0; i < this.fields().length; i++) {
        if (this.fields()[i].value() != undefined) {
            if (parameters !== "?") {
                parameters += "&";
            }

            parameters += this.fields()[i].name + "=" + this.fields()[i].value();
        }
    }

    if (parameters === "?")
        return "";

    return parameters;
};


SirenAction.prototype.post = function (href, payload) {
    $.ajax({
        type: "POST",
        url: href,
        data: payload,
        success: this.handleSuccess.bind(this),
        error: this.handleError.bind(this),
        contentType: "application/x-www-form-urlencoded",
        dataType: "json"
    });
};

SirenAction.prototype.patch = function (href, payload) {
    $.ajax({
        type: "PATCH",
        url: href,
        data: payload,
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

            parameters += this.fields()[i].name + "=" + this.fields()[i].value();
        }
    }

    return parameters;
};


SirenAction.prototype.handleSuccess = function (data) {
    if (_.contains(data.class, "error")) {
        this.serverError(data.properties["error Message"]);
    } else {
        this.postbox.notifySubscribers(data, "refresh");
    }
};

SirenAction.prototype.handleError = function (jqXHR, textStatus, errorThrown) {
    alert("There was an error processing your request: \n\n" + jqXHR.responseText);
};