
Ext.define('Posts.store.PostTypes', {
    extend: 'Ext.data.Store',
    model: 'Posts.model.PostType',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true

});