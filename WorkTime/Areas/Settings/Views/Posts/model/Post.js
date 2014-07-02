
Ext.define('Posts.model.Post', {
    extend: 'Ext.data.Model',

    requires: [
        'Common.reader.Default',
        'Common.writer.Default'
    ],

    fields: [{ 
        name: 'Id' ,
        type: 'int'
    }, {
        name: 'Name',
        type: 'string'
    }],

    idProperty: 'Id',

    proxy: {
        type: 'rest',
        api: {
            create: Application.Path.Get('Settings/Posts/Add'),
            read: Application.Path.Get('Settings/Posts/Get'),
            update: Application.Path.Get('Settings/Posts/Update'),
            destroy: Application.Path.Get('Settings/Posts/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }

});