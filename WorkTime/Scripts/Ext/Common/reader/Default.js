
Ext.define('Common.reader.Default', {
    extend: 'Ext.data.reader.Json',
    alias: ['reader.DefaultReader'],
    root: 'items',
    messageProperty: 'message'
});