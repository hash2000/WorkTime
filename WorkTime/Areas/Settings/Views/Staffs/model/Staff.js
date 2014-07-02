
Ext.define('Staffs.model.Staff', {
    extend: 'Ext.data.Model',

    requires: [
        'Common.reader.Default',
        'Common.writer.Default'
    ],

    fields: [
        { name: 'FullName', type: 'string' },
        { name: 'RegNormId', type: 'int' },
        { name: 'RegNormName', type: 'string' },
        { name: 'RegNormGraphTypeId', type: 'int' },
        { name: 'RegNormGraphTypeName', type: 'string' },
        { name: 'GenderId', type: 'int' },
        { name: 'GenderName', type: 'string' },
        { name: 'EndDate', type: 'date', dateFormat: 'c' },
        { name: 'StartDate', type: 'date', dateFormat: 'c' },
        { name: 'PostTypeName', type: 'string' },
        { name: 'PostTypeId', type: 'int' },
        { name: 'PostName', type: 'string' },
        { name: 'PostId', type: 'int' },
        { name: 'TabelNumber', type: 'int' },
        { name: 'PatronymicName', type: 'string' },
        { name: 'Name', type: 'string' },
        { name: 'Surname', type: 'string' },
        { name: 'Id', type: 'int' }
    ],

    idProperty: 'Id',

    proxy: {
        type: 'rest',
        api: {
            create: Application.Path.Get('Settings/Staffs/Add'),
            read: Application.Path.Get('Settings/Staffs/GetStaffs'),
            update: Application.Path.Get('Settings/Staffs/Update'),
            destroy: Application.Path.Get('Settings/Staffs/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }

});