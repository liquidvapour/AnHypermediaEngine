SelfSubmitSirenAction.prototype = Object.create(SirenAction.prototype);
SelfSubmitSirenAction.prototype.constructor = SelfSubmitSirenAction;

function SelfSubmitSirenAction(postbox, action) {
    this.postbox = postbox;

    this.title = action.title;
    this.name = action.name;

    this.method = action.method;
    this.href = ko.observable();

    this.type = action.type;
    this.fields = ko.observableArray();

    this.serverError = ko.observable();
    this.load(action);

    return this;
};

SelfSubmitSirenAction.prototype.load = function (action) {
    this.href(action.href);

    if (action.fields) {
        this.fields.remove(function (field) { return !_.any(action.fields, function (responseField) { return field.name === responseField.name; }); });
        for (var fieldIndex = 0; fieldIndex < action.fields.length; fieldIndex++) {
            var currentField = _.filter(this.fields(), function (field) { return field.name === action.fields[fieldIndex].name; })[0];

            if (currentField) {
                currentField.load(action.fields[fieldIndex]);
            } else {
                var newField = new SirenField(action.fields[fieldIndex]);
                newField.value.extend({ validatable: false });

                newField.value.subscribe(function(value) {
                    if (value !== undefined) {
                        this.execute();
                    }
                }.bind(this));

                this.fields.push(newField);
            }
        }
    }
};

SelfSubmitSirenAction.prototype.isValid = function() {
    return true;
};