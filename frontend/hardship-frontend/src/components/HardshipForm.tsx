import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useEffect, useState } from "react";
import {
  createHardshipApplication,
  updateHardshipApplication,
} from "../api/hardshipApi";
import type { CreateHardshipRequest, HardshipApplication } from "../types/hardship";

interface Props {
  selected?: HardshipApplication | null;
  onClear: () => void;
}

interface FormErrors {
  customerName?: string;
  dateOfBirth?: string;
  income?: string;
  expenses?: string;
  hardshipReason?: string;
}

export function HardshipForm({ selected, onClear }: Props) {
  const queryClient = useQueryClient();

  const [form, setForm] = useState({
    customerName: "",
    dateOfBirth: "",
    income: "",
    expenses: "",
    hardshipReason: "",
  });

  const [errors, setErrors] = useState<FormErrors>({});

  useEffect(() => {
    if (selected) {
      setForm({
        customerName: selected.customerName,
        dateOfBirth: selected.dateOfBirth.slice(0, 10),
        income: selected.income.toString(),
        expenses: selected.expenses.toString(),
        hardshipReason: selected.hardshipReason ?? "",
      });
      setErrors({});
    }
  }, [selected]);

  const mutation = useMutation({
    mutationFn: (payload: CreateHardshipRequest) =>
      selected
        ? updateHardshipApplication(selected.id, payload)
        : createHardshipApplication(payload),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["hardships"] });
      onClear();
      setForm({
        customerName: "",
        dateOfBirth: "",
        income: "",
        expenses: "",
        hardshipReason: "",
      });
    },
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setForm(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  const validate = (): boolean => {
    const newErrors: FormErrors = {};

    if (!form.customerName.trim()) {
      newErrors.customerName = "Customer name is required";
    }

    if (!form.dateOfBirth) {
      newErrors.dateOfBirth = "Date of birth is required";
    } else if (new Date(form.dateOfBirth) > new Date()) {
      newErrors.dateOfBirth = "Date of birth cannot be in the future";
    }

    const incomeNum = Number(form.income);
    if (!form.income || incomeNum <= 0) {
      newErrors.income = "Income must be greater than 0";
    }

    const expensesNum = Number(form.expenses);
    if (form.expenses === "" || expensesNum < 0) {
      newErrors.expenses = "Expenses cannot be negative";
    }

    if (form.hardshipReason && form.hardshipReason.length > 500) {
      newErrors.hardshipReason = "Reason must be 500 characters or less";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!validate()) return;

    const payload: CreateHardshipRequest = {
      customerName: form.customerName.trim(),
      dateOfBirth: form.dateOfBirth,
      income: Number(form.income),
      expenses: Number(form.expenses),
      hardshipReason: form.hardshipReason.trim() || undefined,
    };

    mutation.mutate(payload);
  };

  return (
    <form onSubmit={handleSubmit} className="card" style={{ padding: 24, borderRadius: 8, boxShadow: "0 0 8px rgba(0,0,0,0.1)" }}>
      <h2>{selected ? "Edit Application" : "Create Application"}</h2>

      <div style={{ marginBottom: 12 }}>
        <input
          name="customerName"
          placeholder="Customer Name"
          value={form.customerName}
          onChange={handleChange}
        />
        {errors.customerName && <div style={{ color: "red" }}>{errors.customerName}</div>}
      </div>

      <div style={{ marginBottom: 12 }}>
        <input
          type="date"
          name="dateOfBirth"
          value={form.dateOfBirth}
          onChange={handleChange}
        />
        {errors.dateOfBirth && <div style={{ color: "red" }}>{errors.dateOfBirth}</div>}
      </div>

      <div style={{ marginBottom: 12 }}>
        <input
          type="number"
          name="income"
          placeholder="Income"
          value={form.income}
          onChange={handleChange}
        />
        {errors.income && <div style={{ color: "red" }}>{errors.income}</div>}
      </div>

      <div style={{ marginBottom: 12 }}>
        <input
          type="number"
          name="expenses"
          placeholder="Expenses"
          value={form.expenses}
          onChange={handleChange}
        />
        {errors.expenses && <div style={{ color: "red" }}>{errors.expenses}</div>}
      </div>

      <div style={{ marginBottom: 12 }}>
        <textarea
          name="hardshipReason"
          placeholder="Hardship Reason (optional)"
          value={form.hardshipReason}
          onChange={handleChange}
        />
        {errors.hardshipReason && <div style={{ color: "red" }}>{errors.hardshipReason}</div>}
      </div>

      <div style={{ display: "flex", gap: 8 }}>
        <button type="submit" disabled={mutation.isPending}>
          {mutation.isPending ? "Submitting..." : selected ? "Update" : "Submit"}
        </button>
        {selected && (
          <button type="button" onClick={onClear}>
            Cancel
          </button>
        )}
      </div>

      {mutation.isError && (
        <p style={{ color: "red", marginTop: 12 }}>
          Failed to submit hardship application.
        </p>
      )}
    </form>
  );
}
