function MultiSelectFieldOption(sirenMultiSelectFieldOption, isSelected) {
    this.isSelected = ko.observable(isSelected);
    
    ko.mapping.fromJS(sirenMultiSelectFieldOption, {}, this);

    return this;
};