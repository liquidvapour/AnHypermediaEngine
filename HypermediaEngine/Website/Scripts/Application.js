function Application() {
    this.root = ko.observable(null);
    this.currentResponse = ko.observable(null);

    this.errorMessage = ko.observable();

    postbox.subscribe(this.handleResponse.bind(this), null, "application");

    return this;
};

Application.prototype.handleResponse = function (response) {
    this.errorMessage(undefined);

    if (_.contains(response.class, "error")) {
        this.errorMessage(response.properties["error Message"]);
    }
    else if (_.contains(response.class, "root")) {
        this.loadRoot(response);
    }
    else {
        this.load(response);
    }
};

Application.prototype.loadRoot = function (response) {
    this.root(ko.mapping.fromJS(response, SirenMappings.prototype.EntityMapping));
};

Application.prototype.load = function (response) {
    if (this.currentResponse() && this.currentResponse().href() === response.href) {
        ko.mapping.fromJS(response, this.currentResponse());
        this.currentResponse().reset();
    } else {
        if (this.currentResponse()) {
            this.currentResponse().dispose();
        }

        this.currentResponse(ko.mapping.fromJS(response, SirenMappings.prototype.EntityMapping));
    }
};