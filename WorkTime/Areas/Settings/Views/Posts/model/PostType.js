

Ext.define('Posts.model.PostType', {
    extend: 'Ext.data.Model',

    requires: [
        'Common.reader.Default',
        'Common.writer.Default'
    ],

    fields: [{
        name: 'Id',
        type: 'int'
    }, {
        name: 'Name',
        type: 'string'
    }, {
        name: 'CoefficientId',
        type: 'int'
    }, {
        name: 'CoefficientName',
        type: 'string'
    }],

    idProperty: 'Id',

    proxy: {
        type: 'rest',
        api: {
            create: Application.Path.Get('Settings/PostTypes/AddPostType'),
            read: Application.Path.Get('Settings/PostTypes/GetPostType'),
            update: Application.Path.Get('Settings/PostTypes/UpdatePostType'),
            destroy: Application.Path.Get('Settings/PostTypes/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }

});