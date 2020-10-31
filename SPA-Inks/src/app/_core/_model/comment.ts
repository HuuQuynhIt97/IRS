import { DateTime } from '@syncfusion/ej2-angular-charts';

export interface ICommentModelName  {
    id: number;
    content: string;
    scheduleID: number;
    createdByName: string;
    createdDate: Date;
    createdBy: number;

}
