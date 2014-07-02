

Ext.define('TimeNorms.store.PostTypesDayTimeCoefficients', {
    extend: 'Ext.data.Store',
    model: 'TimeNorms.model.PostTypesDayTimeCoefficient',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true

});