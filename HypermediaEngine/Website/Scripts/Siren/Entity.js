function SirenEntity(sirenResponse, entityAddress) {
    this.subscriptions = [];
    this.entityAddress = ko.observable(entityAddress);

    ko.mapping.fromJS(sirenResponse, SirenMappings.prototype.EntityMapping, this);

    this.title = ko.observable(_.first(this.class()));

    this.activeAction = ko.observable();

    this.subscriptions.push(postbox.subscribe(this.handleResponse.bind(this), null, this.href()));
    
    return this;
};


SirenEntity.prototype.isVisible = function () {
    return !this.activeAction();
};

SirenEntity.prototype.isActionActive = function (action) {
    return this.activeAction() == action;
};

SirenEntity.prototype.activateAction = function (action) {
    this.activeAction(action);
};

SirenEntity.prototype.deactivateAction = function (action) {
    this.activeAction(undefined);
};

SirenEntity.prototype.reset = function () {
    this.deactivateAction();
};

SirenEntity.prototype.refresh = function () {
    new SirenLink({ href: this.href() }, this.href()).execute();
};

SirenEntity.prototype.handleResponse = function (response) {
    if (_.contains(response.class, "success")) {
        this.refresh();
    }
    else {
        postbox.notifySubscribers(response, this.entityAddress());
    }
};


SirenEntity.prototype.dispose = function() {
    ko.utils.arrayForEach(this.subscriptions, this.disposeEach);
    ko.utils.objectForEach(this, this.disposeEach);
};

SirenEntity.prototype.disposeEach = function (propOrValue, value) {
    var disposable = value || propOrValue;

    if (disposable && typeof disposable.dispose === "function") {
        disposable.dispose();
    }
};