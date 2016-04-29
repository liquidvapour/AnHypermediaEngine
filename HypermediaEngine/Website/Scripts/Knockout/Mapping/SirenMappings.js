function SirenMappings() {
    return this;
};

SirenMappings.prototype.EntityMapping = {
    key: function (data) {
        return data.href;
    },
    create: function (options) {
        var parentAddress = "application";
        
        if (_.contains(options.data.class, "root")) {
            return new Root(options.data, parentAddress);
        }
        else if (_.contains(options.data.class, "login")) {
            return new Unauthorised(options.data, parentAddress);
        }
        else if (_.contains(options.data.class, "films")) {
            return new FilmsCollection(options.data, parentAddress);
        }
        else if (_.contains(options.data.class, "film")) {
            return new FilmsCollectionItem(options.data, parentAddress);
        }
        else if (_.contains(options.data.class, "collection")) {
            return new CollectionEntity(options.data, parentAddress);
        }
        else {
            return new SirenEntity(options.data, parentAddress);
        }
    },
    "entities": {
        key: function (data) {
            return data.href;
        },
        create: function (options) {
            var parentAddress = options.parent.href();

            if (options.parent instanceof CollectionEntity) {
                return new CollectionEntityItem(options.data, parentAddress);
            }
            else {
                return new SirenEntity(options.data, parentAddress);
            }
        }
    },
    "properties": {
        create: function (options) {
            return new SirenProperties(options.data);
        }
    },
    "links": {
        create: function (options) {
            var parentAddress = options.parent.href();
            return new SirenLink(options.data, parentAddress);
        }
    },
    "actions": {
        create: function (options) {
            var parentAddress = options.parent.href();
            return new SirenAction(options.data, parentAddress);
        },
        "ignore": ["fields"]
    }
};

SirenMappings.prototype.ActionMapping = {
    "fields": {
        create: function (options) {
            if (options.data.type === "select") {
                return new SirenMultiSelectField(options.data);
            }

            return new SirenField(options.data);
        }
    }
};