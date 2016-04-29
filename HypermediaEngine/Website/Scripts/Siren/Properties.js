function SirenProperties(data) {
    ko.mapping.fromJS(data, {}, this);
    return this;
};