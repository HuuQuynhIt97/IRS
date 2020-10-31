export interface IChemical  {
    id: number;
    name: string;
    code: string;
    createdDate: Date;
    voc: number;
    supplierID: number;
    allow: number;
    processID: number;
    expiredTime: number;
    createdBy: number;
    modify: boolean;
    daysToExpiration: number;
    materialNO: string;
    unit: number;
}
