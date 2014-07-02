

Ext.define('Posts.model.VacationTypeInPostType', {
    extend: 'Ext.data.Model',

    requires: [
        'Common.reader.Default',
        'Common.writer.Default'
    ],

    fields: [
        { name: 'Id', type: 'int' },
        { name: 'Label', type: 'string' },
        { name: 'Name', type: 'string' },
        { name: 'IsUsed', type: 'boolean' }
    ],
    idProperty: 'Id',
    proxy: {
        type: 'rest',
        api: {
            read: Application.Path.Get('Settings/PostTypes/GetVacationTypesInPostTypes'),
            update: Application.Path.Get('Settings/PostTypes/UpdateVacationTypesInPostTypes'),
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }
});



