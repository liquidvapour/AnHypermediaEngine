FilmsCollectionItem.prototype = Object.create(SirenEntity.prototype);
FilmsCollectionItem.prototype.constructor = FilmsCollectionItem;

function FilmsCollectionItem(sirenResponse, entityAddress) {
    this.subscriptions = [];
    this.entityAddress = ko.observable(entityAddress);

    ko.mapping.fromJS(sirenResponse, SirenMappings.prototype.EntityMapping, this);

    this.title = ko.observable(_.first(this.class()));

    this.activeAction = ko.observable();
    
    this.detailsLink = ko.observable();
    this.deleteAction = ko.observable();

    if (this.links) { 
        this.detailsLink(_.find(this.links(), function (link) { return _.contains(link.rel(), "detail"); }));
    }
    
    if (this.actions) {
        this.deleteAction(_.find(this.actions(), function (action) { return action.title() == "Delete"; }));
    }

    this.subscriptions.push(postbox.subscribe(this.handleResponse.bind(this), null, this.href()));

    return this;
};

FilmsCollectionItem.prototype.handleResponse = function (response) {
    if (_.contains(response.class, "success")) {
        this.refresh();
    }

    postbox.notifySubscribers(response, this.entityAddress());
};