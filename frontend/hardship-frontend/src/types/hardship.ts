export interface HardshipApplication {
  id: number
  customerName: string
  dateOfBirth: string
  income: number
  expenses: number
  hardshipReason?: string | null
  createdAt: string
  updatedAt: string
}

export interface CreateHardshipRequest {
  customerName: string
  dateOfBirth: string
  income: number
  expenses: number
  hardshipReason?: string
}
