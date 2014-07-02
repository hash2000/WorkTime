

Ext.define('Common.writer.Default', {
    extend: 'Ext.data.writer.Json',
    alias: ['writer.DefaultWriter'],
    type: 'json',
    root: 'item',
    allowSingle: true,
    encode: false
});