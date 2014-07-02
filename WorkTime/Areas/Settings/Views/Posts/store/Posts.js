
Ext.define('Posts.store.Posts', {
    extend: 'Ext.data.Store',
    model: 'Posts.model.Post',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true

});