

Ext.define('TimeNorms.store.RegNorms', {
    extend: 'Ext.data.Store',
    model: 'TimeNorms.model.RegNorm',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true,
    //modelDefaults: {
    //    // значение выставляется в инициализации viewport
    //    Year: 0
    //}

});