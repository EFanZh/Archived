int _tmain(int argc, TCHAR *argv[], TCHAR *envp[])
{
    envp;

    if (argc == 2)
    {
        if (NvAPI_Initialize() == NVAPI_OK)
        {
            NvU32 display_id;
            if (NvAPI_DISP_GetGDIPrimaryDisplayId(&display_id) == NVAPI_OK)
            {
                int index = _tstoi(argv[1]);
                NV_CUSTOM_DISPLAY custom_display = { NV_CUSTOM_DISPLAY_VER };

                if (NvAPI_DISP_EnumCustomDisplay(display_id, index, &custom_display) == NVAPI_OK)
                {
                    NvAPI_DISP_TryCustomDisplay(&display_id, 1, &custom_display);
                }
            }
            NvAPI_Unload();
        }
    }
}
