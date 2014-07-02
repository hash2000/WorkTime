
Ext.define('Staffs.model.Gender', {
    extend: 'Ext.data.Model',

    requires: [
        'Common.reader.Default',
        'Common.writer.Default'
    ],

    fields: [
        { name: 'Name', type: 'string' },
        { name: 'Id', type: 'int' }
    ],

    idProperty: 'Id',

    proxy: {
        type: 'rest',
        api: {
            create: Application.Path.Get('Settings/Genders/Add'),
            read: Application.Path.Get('Settings/Genders/Get'),
            update: Application.Path.Get('Settings/Genders/Update'),
            destroy: Application.Path.Get('Settings/Genders/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }

});