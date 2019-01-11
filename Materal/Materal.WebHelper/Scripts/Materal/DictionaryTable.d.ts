declare namespace Materal {
    class DictionaryTable {
        private readonly tableElement;
        private readonly tableBodyElement;
        private readonly targetRow;
        private readonly data;
        private readonly config;
        constructor(elementId: string, data: Dictionary, config: DictionaryTableConfig);
        updateTable(): void;
    }
    class DictionaryTableConfig {
        showNumber: number;
    }
}
