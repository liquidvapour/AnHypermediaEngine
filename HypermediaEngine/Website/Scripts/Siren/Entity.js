function SirenEntity(postbox, response) {
    this.href = ko.observable();
    this.class = ko.observableArray();
    this.properties = ko.observableArray();
    this.entities = ko.observableArray();
    this.actions = ko.observableArray();
    this.links = ko.observableArray();

    this.title = ko.observable();

    this.postbox = postbox;

    this.load(response);

    return this;
};

SirenEntity.prototype.load = function (response) {
    this.href(response.href);
    this.class(response.class);

    if (response.properties) {
        this.title(response.properties["title"]);
    } else {
        this.title(response.class[0]);
    }

    if (response.links) {
        this.links.remove(function (link) { return !_.any(response.links, function (responseLink) { return link.href === responseLink.href; }); });
        for (var linkIndex = 0; linkIndex < response.links.length; linkIndex++) {
            var currentLink = _.filter(this.links(), function (link) { return link.href === response.links[linkIndex].href; })[0];

            if (currentLink) {
                currentLink.load(response.links[linkIndex]);
            } else {
                this.links.push(new SirenLink(this.postbox, response.links[linkIndex]));
            }
        }
    } else {
        this.links([]);
    }

    if (response.actions) {
        this.actions.remove(function(action) { return !_.any(response.actions, function(responseAction) { return action.name === responseAction.name; }); });
        for (var actionIndex = 0; actionIndex < response.actions.length; actionIndex++) {
            var currentAction = _.filter(this.actions(), function(action) { return action.name === response.actions[actionIndex].name; })[0];

            if (currentAction) {
                currentAction.load(response.actions[actionIndex]);
            } else {
                this.actions.push(new SirenAction(this.postbox, response.actions[actionIndex]));
            }
        }
    } else {
        this.actions([]);
    }

    if (response.properties) {
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
                    var currentProperty = _.filter(this.properties(), function(property) { return property.key === key; })[0];
                    if (currentProperty) {
                        currentProperty.value(response.properties[key]);
                    } else {
                        this.properties.push({ key: key, value: ko.observable(response.properties[key]) });
                    }
                }
            }
        }
    } else {
        this.properties([]);
    }

    if (response.entities) {
        for (var entityIndex = 0; entityIndex < response.entities.length; entityIndex++) {
            this.entities.push(new SirenEntity(this.postbox, response.entities[entityIndex]));
        }
    } else {
        this.entities([]);
    }
};