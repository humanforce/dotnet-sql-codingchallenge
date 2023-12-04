import { Guid } from 'guid-typescript';

export class carsModel{
    id: Guid | undefined;
    name: string | undefined;
    colour: string | undefined;
    price: number | undefined;
}