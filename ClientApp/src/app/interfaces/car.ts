export interface QueryResult {
    size: number,
    items: Car[]
}

export interface Car {
    id: number,
    price: number,
    contact: Contact
    year: number,
    model: Model,
    thumbnail: string,
    make: Make,
    registered: boolean,
    features: Feature[]
}


export interface Contact {
    name: string,
    email: string,
}

export interface Make {
    id: number,
    name: string,
    models: Model[]
}

export interface Model {
    id: number,
    name: string,
}


export interface Feature {
    id: number,
    name: string,
}