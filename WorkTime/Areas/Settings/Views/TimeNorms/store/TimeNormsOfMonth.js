

Ext.define('TimeNorms.store.TimeNormsOfMonth', {
    extend: 'Ext.data.Store',
    model: 'TimeNorms.model.TimeNormOfMonth',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true,
    modelDefaults: {
        // значение выставляется в инициализации viewport
        Year: 0
    }


});