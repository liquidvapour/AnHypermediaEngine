function Menu(links, actions) {
    this.links = links;
    this.actions = actions;

    this.activeLink = ko.observable();

    return this;
};

Menu.prototype.isActiveLink = function (item) {
    return this.activeLink() === item;
};

Menu.prototype.executeLink = function (link) {
    this.activeLink(link);
    link.execute();
};