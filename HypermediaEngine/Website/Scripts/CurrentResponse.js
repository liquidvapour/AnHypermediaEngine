CurrentResponse.prototype = Object.create(SirenEntity.prototype);
CurrentResponse.prototype.constructor = CurrentResponse;

function CurrentResponse(response) {
    this.href = ko.observable();
    this.class = ko.observableArray();
    this.properties = ko.observableArray();
    this.entities = ko.observableArray();
    this.actions = ko.observableArray();
    this.links = ko.observableArray();

    this.menu = ko.observable();
    this.title = ko.observable();

    this.postbox = new ko.subscribable();
    this.postbox.subscribe(this.load.bind(this), null, "refresh");

    this.load(response);

    return this;
};

CurrentResponse.prototype.load = function (response) {
    if (response == null)
        return;
    
    this.href(response.href);
    this.class(response.class);

    if (response.properties) {
        this.title(response.properties["title"]);
    } else {
        this.title(response.class[0]);
    }

    var homeLink = _.filter(response.links, function (link) { return _.contains(link.rel, "home"); })[0];
    var menuLinks = _.filter(response.links, function (link) { return _.contains(link.rel, "register") || _.contains(link.rel, "login") || _.contains(link.rel, "remittances-list"); });
    var menuActions = _.filter(response.actions, function (action) { return action.name === "language" || action.name === "logout"; });
    if (this.menu()) {
        this.menu().load(homeLink, menuLinks, menuActions);
    } else {
        this.menu(new Menu(this.postbox));
        this.menu().load(homeLink, menuLinks, menuActions);
    }

    var responseLinks = _.filter(response.links, function (link) { return _.contains(link.rel, "home") === false && _.contains(link.rel, "register") === false && _.contains(link.rel, "login") === false && _.contains(link.rel, "remittances-list") === false; });
    this.links.remove(function (link) { return !_.any(responseLinks, function (responseLink) { return link.href === responseLink.href; }); });
    for (var linkIndex = 0; linkIndex < responseLinks.length; linkIndex++) {
        var currentLink = _.filter(this.links(), function (link) { return link.href === responseLinks[linkIndex].href; })[0];

        if (currentLink) {
            currentLink.load(responseLinks[linkIndex]);
        } else {
            this.links.push(new SirenLink(this.postbox, responseLinks[linkIndex]));
        }
    }

    var responseActions = _.filter(response.actions, function (action) { return action.name !== "language" && action.name !== "logout"; });
    this.actions.remove(function (action) { return !_.any(responseActions, function (responseAction) { return action.name === responseAction.name; }); });
    for (var actionIndex = 0; actionIndex < responseActions.length; actionIndex++) {
        var currentAction = _.filter(this.actions(), function (action) { return action.name === responseActions[actionIndex].name; })[0];

        if (currentAction) {
            currentAction.load(responseActions[actionIndex]);
        } else {
            if (responseActions[actionIndex].name === "register" || responseActions[actionIndex].name === "login" || responseActions[actionIndex].name === "send-now" || responseActions[actionIndex].name === "pay-now" || responseActions[actionIndex].name === "add-recipient" || responseActions[actionIndex].name === "update-recipient") {
                this.actions.push(new SirenAction(this.postbox, responseActions[actionIndex]));
            } else {
                this.actions.push(new SelfSubmitSirenAction(this.postbox, responseActions[actionIndex]));
            }
        }
    }

    this.properties.remove(function(property) {
        for (var key in response.properties) {
            if (key === property.key) {
                return false;
            }
        }

        return true;
    });

    for (var key in response.properties) {
        if (response.properties.hasOwnProperty(key)) {
            if (key !== "title") {
                var currentProperty = _.filter(this.properties(), function (property) { return property.key === key; })[0];
                if (currentProperty) {
                    currentProperty.value(response.properties[key]);
                } else {
                    this.properties.push({ key: key, value: ko.observable(response.properties[key]) });
                }
            }
        }
    }

    if (response.entities) {
        this.entities.remove(function (entity) { return !_.any(response.entities, function (responseEntity) { return entity.href() === responseEntity.href; }); });
        for (var entityIndex = 0; entityIndex < response.entities.length; entityIndex++) {
            var currentEntity = _.filter(this.entities(), function(entity) { return entity.href() === response.entities[entityIndex].href; })[0];

            if (currentEntity) {
                currentEntity.load(response.entities[entityIndex]);
            } else {
                this.entities.push(new SirenEntity(this.postbox, response.entities[entityIndex]));
            }
        }
    } else {
        this.entities([]);
    }
};
