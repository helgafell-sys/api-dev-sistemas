using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Consultorio.Desktop.Models;
using Consultorio.Desktop.Services;

namespace Consultorio.Desktop.ViewModels;

public partial class PatientsViewModel : ObservableObject
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private ObservableCollection<PatientDto> patients = new();

    [ObservableProperty]
    private PatientDto? selectedPatient;

    [ObservableProperty]
    private string patientName = string.Empty;

    [ObservableProperty]
    private string patientEmail = string.Empty;

    [ObservableProperty]
    private string statusMessage = string.Empty;

    [ObservableProperty]
    private bool isLoading = false;

    public PatientsViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [RelayCommand]
    public async Task LoadPatients()
    {
        try
        {
            IsLoading = true;
            StatusMessage = "Carregando pacientes...";
            var list = await _apiClient.GetAllAsync();
            Patients.Clear();
            foreach (var p in list) Patients.Add(p);
            StatusMessage = $"✓ {list.Count} paciente(s) carregado(s)";
        }
        catch (Exception ex)
        {
            StatusMessage = $"✗ Erro: {ex.Message}";
        }
        finally { IsLoading = false; }
    }

    [RelayCommand]
    public async Task CreatePatient()
    {
        if (string.IsNullOrWhiteSpace(PatientName) || string.IsNullOrWhiteSpace(PatientEmail))
        {
            StatusMessage = "✗ Preencha todos os campos";
            return;
        }
        try
        {
            IsLoading = true;
            var dto = new PatientCreateDto { Name = PatientName, Email = PatientEmail };
            var (success, error, created) = await _apiClient.CreateAsync(dto);
            if (success && created != null)
            {
                Patients.Add(created);
                ClearForm();
                StatusMessage = $"✓ Paciente '{created.Name}' criado!";
            }
            else StatusMessage = $"✗ Erro: {error}";
        }
        catch (Exception ex) { StatusMessage = $"✗ Erro: {ex.Message}"; }
        finally { IsLoading = false; }
    }

    [RelayCommand]
    public async Task UpdatePatient()
    {
        if (SelectedPatient == null || string.IsNullOrWhiteSpace(PatientName))
        {
            StatusMessage = "✗ Selecione um paciente e preencha os campos";
            return;
        }
        try
        {
            IsLoading = true;
            var dto = new PatientUpdateDto { Name = PatientName, Email = PatientEmail };
            var (success, error) = await _apiClient.UpdateAsync(SelectedPatient.Id, dto);
            if (success)
            {
                SelectedPatient.Name = PatientName;
                SelectedPatient.Email = PatientEmail;
                StatusMessage = "✓ Paciente atualizado!";
            }
            else StatusMessage = $"✗ Erro: {error}";
        }
        catch (Exception ex) { StatusMessage = $"✗ Erro: {ex.Message}"; }
        finally { IsLoading = false; }
    }

    [RelayCommand]
    public async Task DeletePatient()
    {
        if (SelectedPatient == null) return;
        try
        {
            IsLoading = true;
            var (success, error) = await _apiClient.DeleteAsync(SelectedPatient.Id);
            if (success)
            {
                Patients.Remove(SelectedPatient);
                ClearForm();
                StatusMessage = "✓ Paciente removido!";
            }
            else StatusMessage = $"✗ Erro: {error}";
        }
        catch (Exception ex) { StatusMessage = $"✗ Erro: {ex.Message}"; }
        finally { IsLoading = false; }
    }

    [RelayCommand]
    public void ClearForm()
    {
        PatientName = string.Empty;
        PatientEmail = string.Empty;
        SelectedPatient = null;
    }

    partial void OnSelectedPatientChanged(PatientDto? value)
    {
        if (value != null)
        {
            PatientName = value.Name;
            PatientEmail = value.Email;
        }
    }
}