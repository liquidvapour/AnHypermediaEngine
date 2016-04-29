Unauthorised.prototype = Object.create(ActionEntity.prototype);
Unauthorised.prototype.constructor = Unauthorised;

function Unauthorised(sirenResponse, entityAddress) {
    this.subscriptions = [];
    this.entityAddress = ko.observable(entityAddress);

    ko.mapping.fromJS(sirenResponse, SirenMappings.prototype.EntityMapping, this);

    this.title = ko.observable(_.first(this.class()));

    this.defaultAction = ko.observable();
    this.activeAction = ko.observable();

    if (this.actions) {
        this.defaultAction(_.find(this.actions(), function (action) { return this.title() == action.name(); }.bind(this)));
        this.activeAction(this.defaultAction());
    }

    this.subscriptions.push(postbox.subscribe(this.handleResponse.bind(this), null, this.href()));
    
    return this;
};

Unauthorised.prototype.handleResponse = function (response) {
    if (_.contains(response.class, "login") && _.contains(response.class, "success")) {
        document.cookie = "accessToken=" + response.properties["access Token"];
        location.reload();
    }
    else {
        postbox.notifySubscribers(response, this.entityAddress());
    }
};