function Menu(postbox) {
    this.postbox = postbox;

    this.home = ko.observable();
    this.links = ko.observableArray();
    this.actions = ko.observableArray();

    return this;
};

Menu.prototype.load = function (home, links, actions) {
    if (this.home()) {
        this.home().load(home);
    } else {
        this.home(new SirenLink(this.postbox, home));
    }

    this.links.remove(function (link) { return !_.any(links, function (responseLink) { return link.href === responseLink.href; }); });
    for (var linkIndex = 0; linkIndex < links.length; linkIndex++) {
        var currentLink = _.filter(this.links(), function (link) { return link.rel[0] === links[linkIndex].rel[0]; })[0];

        if (currentLink) {
            currentLink.load(links[linkIndex]);
        } else {
            this.links.push(new SirenLink(this.postbox, links[linkIndex]));
        }
    }

    this.actions.remove(function (action) { return !_.any(actions, function (responseAction) { return action.name === responseAction.name; }); });
    for (var actionIndex = 0; actionIndex < actions.length; actionIndex++) {
        var currentAction = _.filter(this.actions(), function (action) { return action.name === actions[actionIndex].name; })[0];

        if (currentAction) {
            currentAction.load(actions[actionIndex]);
        } else {
            if (actions[actionIndex].name === "logout") {
                this.actions.push(new SirenAction(this.postbox, actions[actionIndex]));
            } else {
                this.actions.push(new SelfSubmitSirenAction(this.postbox, actions[actionIndex]));
            }
        }
    }
};
