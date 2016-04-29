function CollectionEntity(sirenResponse, entityAddress) {
    this.subscriptions = [];

    this.entityAddress = ko.observable(entityAddress);

    ko.mapping.fromJS(sirenResponse, SirenMappings.prototype.EntityMapping, this);

    this.title = ko.observable(_.first(this.class()));

    this.subscriptions.push(postbox.subscribe(this.handleResponse.bind(this), null, this.href()));
    
    return this;
};

CollectionEntity.prototype.reset = function () {
};

CollectionEntity.prototype.refresh = function () {
    new SirenLink({ href: this.href() }, this.href()).execute();
};

CollectionEntity.prototype.handleResponse = function (response) {
    if (_.contains(response.class, "success")) {
        this.refresh();
    }
    else {
      postbox.notifySubscribers(response, this.entityAddress());
    }
};


CollectionEntity.prototype.dispose = function () {
    ko.utils.arrayForEach(this.subscriptions, this.disposeEach);
    ko.utils.objectForEach(this, this.disposeEach);
};

CollectionEntity.prototype.disposeEach = function (propOrValue, value) {
    var disposable = value || propOrValue;

    if (disposable && typeof disposable.dispose === "function") {
        disposable.dispose();
    }
};