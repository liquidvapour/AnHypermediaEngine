function SirenField(sirenField) {
    this.subscriptions = [];
    
    this.value = ko.observable();
    
    ko.mapping.fromJS(sirenField, {}, this);

    if (sirenField.required == true) {
        this.value.extend({ required: true });
    }
    
    if (sirenField.type == 'email') {
        this.value.extend({ email: true });
    }

    if (sirenField.type == 'number') {
        this.value.extend({ number: true });

        if (sirenField.min) {
            this.value.extend({ min: sirenField.min });
        }
    
        if (sirenField.step) {
            this.value.extend({ step: sirenField.step });
        }
    }

    return this;
};

SirenField.prototype.dispose = function () {
    ko.utils.arrayForEach(this.subscriptions, this.disposeEach);
    ko.utils.objectForEach(this, this.disposeEach);
};

SirenField.prototype.disposeEach = function (propOrValue, value) {
    var disposable = value || propOrValue;

    if (disposable && typeof disposable.dispose === "function") {
        disposable.dispose();
    }
};