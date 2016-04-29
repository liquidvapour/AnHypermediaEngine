FilmsCollection.prototype = Object.create(CollectionEntity.prototype);
FilmsCollection.prototype.constructor = FilmsCollection;

function FilmsCollection(sirenResponse, entityAddress) {
    this.subscriptions = [];
    this.entityAddress = ko.observable(entityAddress);

    ko.mapping.fromJS(sirenResponse, SirenMappings.prototype.EntityMapping, this);

    this.title = ko.observable(_.first(this.class()));

    this.activeItem = ko.observable();

    this.subscriptions.push(postbox.subscribe(this.handleResponse.bind(this), null, this.href()));
    
    return this;
};

FilmsCollection.prototype.handleResponse = function (response) {
    if (_.contains(response.class, 'film')) {
        if (this.activeItem() && this.activeItem().href() === response.href) {
            ko.mapping.fromJS(response, this.activeItem());
            this.activeItem().reset();
        } else {
            if (this.activeItem()) {
                this.activeItem().dispose();
            }
            
            var film = ko.mapping.fromJS(response, SirenMappings.prototype.EntityMapping);
            film.entityAddress(this.href());
            
            this.activeItem(film);
        }
    } else if (_.contains(response.class, "success")) {
        this.refresh();
    } else {
        postbox.notifySubscribers(response, this.entityAddress());
    }
};