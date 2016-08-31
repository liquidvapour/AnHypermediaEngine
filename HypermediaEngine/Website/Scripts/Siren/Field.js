function SirenField(field) {
    this.type = field.type;

    this.title = field.title;
    this.name = field.name;

    this.value = ko.observable();

    this.min = field.min;
    this.max = field.max;
    this.step = field.step;

    if (field.required === true) {
        this.value.extend({ required: true });
    }
    
    if (field.type === "email") {
        this.value.extend({ email: true });
    }

    if (field.type === "number") {
        this.value.extend({ number: true });

        if (field.min) {
            this.value.extend({ min: field.min });
        }
    
        if (field.step) {
            this.value.extend({ step: field.step });
        }

        if (field.max) {
            this.value.extend({ max: field.max });
        }
    }

    if (field.type === "select") {
        this.options = ko.observableArray();

        if (field.optionsLookup) {
            this.optionsLookupHref = ko.observable();
        }
    }

    this.load(field);

    return this;
};

SirenField.prototype.load = function (field) {
    if (this.value() != field.value) {
        this.value(field.value);
    }

    if (field.type === "select") {
        if (field.options) {
            this.options(field.options);
        }

        if (field.optionsLookup) {
            if (this.optionsLookupHref() !== field.optionsLookup.href) {
                this.optionsLookupHref(field.optionsLookup.href);

                $.ajax({
                    type: "GET",
                    url: field.optionsLookup.href,
                    success: this.loadOptions.bind(this)
                });
            }
        }
    }
};

SirenField.prototype.loadOptions = function (response) {
    var options = [];

    for (var key in response.properties) {
        if (response.properties.hasOwnProperty(key)) {
            options.push({ key: key, value: response.properties[key] });
        }
    }

    this.options(options);
};