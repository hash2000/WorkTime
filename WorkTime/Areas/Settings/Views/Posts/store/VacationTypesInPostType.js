

Ext.define('Posts.store.VacationTypesInPostType', {
    extend: 'Ext.data.Store',
    model: 'Posts.model.VacationTypeInPostType',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true
});
