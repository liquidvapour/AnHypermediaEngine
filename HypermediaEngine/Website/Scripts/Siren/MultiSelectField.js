SirenMultiSelectField.prototype = Object.create(SirenField.prototype);
SirenMultiSelectField.prototype.constructor = SirenMultiSelectField;

function SirenMultiSelectField(sirenField) {
    this.subscriptions = [];

    ko.mapping.fromJS(sirenField, {
        'options': {
            create: function (options) {
                var isSelected = false;
                
                if (_.contains(sirenField.value, options.data.key)) {
                    isSelected = true;
                }
                
                return new MultiSelectFieldOption(options.data, isSelected);
            }
        }
    }, this);

    if (sirenField.required == true) {
        this.value.extend({ required: true });
    }
    
    return this;
};

SirenMultiSelectField.prototype.toggleOption = function (option) {
    if (_.contains(this.value(), option.value())) {
        option.isSelected(false);

        var index = this.value().indexOf(option.value());
        this.value().splice(index, 1);
    }
    else {
        option.isSelected(true);
        this.value().push(option.value());
    }
};