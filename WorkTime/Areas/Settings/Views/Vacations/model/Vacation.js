
Ext.define('Vacations.model.Vacation', {
    extend: 'Ext.data.Model',

    requires: [
       'Common.reader.Default',
       'Common.writer.Default'
    ],

    fields: [
       { name: 'VacationTypeName', type: 'string' },
       { name: 'VacationTypeId', type: 'int' },
       { name: 'StartDate', type: 'date' },
       { name: 'EndDate', type: 'date'},
       { name: 'EndDateWithHolidays', type: 'date' },
       { name: 'StaffName', type: 'string' },
       { name: 'StaffId', type: 'int' },
       { name: 'Id', type: 'int' }
    ],
    idProperty: 'Id',
    proxy: {
        type: 'rest',
        api: {
            read: Application.Path.Get('Settings/Vacations/GetVacations'),
            create: Application.Path.Get('Settings/Vacations/AddVacations'),
            update: Application.Path.Get('Settings/Vacations/UpdateVacations'),
            destroy: Application.Path.Get('Settings/Vacations/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }
});

