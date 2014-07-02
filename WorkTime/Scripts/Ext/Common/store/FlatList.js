

Ext.define('Common.store.FlatList', {
    extend: 'Ext.data.Store',
    fields: ['Value', 'Name'],
    valueFrom: 0,
    valueTo: 1,
    valueStep: 1,
    autoLoad: false,
    names: undefined,
    listeners: {
        load: {
            fn: function () {
                for (var i = this.valueFrom ; i < this.valueTo ; i += this.valueStep) {
                    var obj = {
                        Value: i
                    };
                    if (!Ext.isEmpty(this.names)) {
                        var name = this.names[i];
                        if (!Ext.isEmpty(name))
                            Ext.apply(obj, {
                                Name: name
                            });
                    }
                    this.add(obj);
                }
            }
        }
    }
});