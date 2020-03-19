using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ManagedCuda;
using ManagedCuda.BasicTypes;

namespace TestManagedCUDA.Model
{
    public class CUDARunner
    {

        // 1.Простой тест 
        // 2.Тест с передачей и приемом больших и разных структур
        public string GetSummary()
        {
            string s = "";

            //int deviceID = 0;
            //CudaContext ctx = new CudaContext(deviceID, CUCtxFlags.MapHost | CUCtxFlags.BlockingSync);

            //for default setting with device 0:
            //CudaContext ctx = new CudaContext();

            int deviceCount = CudaContext.GetDeviceCount();

            s += $"deviceCount = {deviceCount}\n";

            for (int deviceID = 0; deviceID < deviceCount; deviceID++)
            {
                s += $"----- DeviceID = {deviceID} -----\n";
                CudaDeviceProperties props = CudaContext.GetDeviceInfo(deviceID);
                s += $"DeviceName = {props.DeviceName}\n";
                s += $"DriverVersion = {props.DriverVersion.ToString()}\n";
                s += $"CUDA ComputeCapability = {props.ComputeCapability.ToString()}\n";
                s += $"ClockRate = {(props.ClockRate/1000).ToString()} MHz\n";
                s += $"TotalGlobalMemory = {(props.TotalGlobalMemory / 1000000).ToString()} Mb\n";
                s += $"MultiProcessorCount = {props.MultiProcessorCount.ToString()}\n";
                s += $"Integrated = {props.Integrated.ToString()}\n";
                s += $"MemoryClockRate = {(props.MemoryClockRate/1000).ToString()} MHz\n";
                s += $"GlobalMemoryBusWidth = {props.GlobalMemoryBusWidth.ToString()} bit\n";
            }
            /*
            + name[256] is an ASCII string identifying the device;
            uuid is a 16-byte unique identifier.
            + totalGlobalMem is the total amount of global memory available on the device in bytes;
            sharedMemPerBlock is the maximum amount of shared memory available to a thread block in bytes;
            regsPerBlock is the maximum number of 32-bit registers available to a thread block;
            warpSize is the warp size in threads;
            memPitch is the maximum pitch in bytes allowed by the memory copy functions that involve memory regions allocated through cudaMallocPitch();
            maxThreadsPerBlock is the maximum number of threads per block;
            maxThreadsDim[3] contains the maximum size of each dimension of a block;
            maxGridSize[3] contains the maximum size of each dimension of a grid;
            + clockRate is the clock frequency in kilohertz;
            totalConstMem is the total amount of constant memory available on the device in bytes;
            major, minor are the major and minor revision numbers defining the device's compute capability;
            textureAlignment is the alignment requirement; texture base addresses that are aligned to textureAlignment bytes do not need an offset applied to texture fetches;
            texturePitchAlignment is the pitch alignment requirement for 2D texture references that are bound to pitched memory;
            deviceOverlap is 1 if the device can concurrently copy memory between host and device while executing a kernel, or 0 if not. Deprecated, use instead asyncEngineCount.
            + multiProcessorCount is the number of multiprocessors on the device;
            kernelExecTimeoutEnabled is 1 if there is a run time limit for kernels executed on the device, or 0 if not.
            + integrated is 1 if the device is an integrated (motherboard) GPU and 0 if it is a discrete (card) component.
            canMapHostMemory is 1 if the device can map host memory into the CUDA address space for use with cudaHostAlloc()/cudaHostGetDevicePointer(), or 0 if not;
            computeMode is the compute mode that the device is currently in. Available modes are as follows:
                cudaComputeModeDefault: Default mode - Device is not restricted and multiple threads can use cudaSetDevice() with this device.
                cudaComputeModeExclusive: Compute-exclusive mode - Only one thread will be able to use cudaSetDevice() with this device.
                cudaComputeModeProhibited: Compute-prohibited mode - No threads can use cudaSetDevice() with this device.
                cudaComputeModeExclusiveProcess: Compute-exclusive-process mode - Many threads in one process will be able to use cudaSetDevice() with this device.
                If cudaSetDevice() is called on an already occupied device with computeMode cudaComputeModeExclusive, cudaErrorDeviceAlreadyInUse will be immediately returned indicating the device cannot be used. When an occupied exclusive mode device is chosen with cudaSetDevice, all subsequent non-device management runtime functions will return cudaErrorDevicesUnavailable.
            maxTexture1D is the maximum 1D texture size.
            maxTexture1DMipmap is the maximum 1D mipmapped texture texture size.
            maxTexture1DLinear is the maximum 1D texture size for textures bound to linear memory.
            maxTexture2D[2] contains the maximum 2D texture dimensions.
            maxTexture2DMipmap[2] contains the maximum 2D mipmapped texture dimensions.
            maxTexture2DLinear[3] contains the maximum 2D texture dimensions for 2D textures bound to pitch linear memory.
            maxTexture2DGather[2] contains the maximum 2D texture dimensions if texture gather operations have to be performed.
            maxTexture3D[3] contains the maximum 3D texture dimensions.
            maxTexture3DAlt[3] contains the maximum alternate 3D texture dimensions.
            maxTextureCubemap is the maximum cubemap texture width or height.
            maxTexture1DLayered[2] contains the maximum 1D layered texture dimensions.
            maxTexture2DLayered[3] contains the maximum 2D layered texture dimensions.
            maxTextureCubemapLayered[2] contains the maximum cubemap layered texture dimensions.
            maxSurface1D is the maximum 1D surface size.
            maxSurface2D[2] contains the maximum 2D surface dimensions.
            maxSurface3D[3] contains the maximum 3D surface dimensions.
            maxSurface1DLayered[2] contains the maximum 1D layered surface dimensions.
            maxSurface2DLayered[3] contains the maximum 2D layered surface dimensions.
            maxSurfaceCubemap is the maximum cubemap surface width or height.
            maxSurfaceCubemapLayered[2] contains the maximum cubemap layered surface dimensions.
            surfaceAlignment specifies the alignment requirements for surfaces.
            concurrentKernels is 1 if the device supports executing multiple kernels within the same context simultaneously, or 0 if not. It is not guaranteed that multiple kernels will be resident on the device concurrently so this feature should not be relied upon for correctness;
            ECCEnabled is 1 if the device has ECC support turned on, or 0 if not.
            pciBusID is the PCI bus identifier of the device.
            pciDeviceID is the PCI device (sometimes called slot) identifier of the device.
            pciDomainID is the PCI domain identifier of the device.
            tccDriver is 1 if the device is using a TCC driver or 0 if not.
            asyncEngineCount is 1 when the device can concurrently copy memory between host and device while executing a kernel. It is 2 when the device can concurrently copy memory between host and device in both directions and execute a kernel at the same time. It is 0 if neither of these is supported.
            unifiedAddressing is 1 if the device shares a unified address space with the host and 0 otherwise.
            + memoryClockRate is the peak memory clock frequency in kilohertz.
            + memoryBusWidth is the memory bus width in bits.
            l2CacheSize is L2 cache size in bytes.
            maxThreadsPerMultiProcessor is the number of maximum resident threads per multiprocessor.
            streamPrioritiesSupported is 1 if the device supports stream priorities, or 0 if it is not supported.
            globalL1CacheSupported is 1 if the device supports caching of globals in L1 cache, or 0 if it is not supported.
            localL1CacheSupported is 1 if the device supports caching of locals in L1 cache, or 0 if it is not supported.
            sharedMemPerMultiprocessor is the maximum amount of shared memory available to a multiprocessor in bytes; this amount is shared by all thread blocks simultaneously resident on a multiprocessor;
            regsPerMultiprocessor is the maximum number of 32-bit registers available to a multiprocessor; this number is shared by all thread blocks simultaneously resident on a multiprocessor;
            managedMemory is 1 if the device supports allocating managed memory on this system, or 0 if it is not supported.
            isMultiGpuBoard is 1 if the device is on a multi-GPU board (e.g. Gemini cards), and 0 if not;
            multiGpuBoardGroupID is a unique identifier for a group of devices associated with the same board. Devices on the same multi-GPU board will share the same identifier;
            singleToDoublePrecisionPerfRatio is the ratio of single precision performance (in floating-point operations per second) to double precision performance.
            pageableMemoryAccess is 1 if the device supports coherently accessing pageable memory without calling cudaHostRegister on it, and 0 otherwise.
            concurrentManagedAccess is 1 if the device can coherently access managed memory concurrently with the CPU, and 0 otherwise.
            computePreemptionSupported is 1 if the device supports Compute Preemption, and 0 otherwise.
            canUseHostPointerForRegisteredMem is 1 if the device can access host registered memory at the same virtual address as the CPU, and 0 otherwise.
            cooperativeLaunch is 1 if the device supports launching cooperative kernels via cudaLaunchCooperativeKernel, and 0 otherwise.
            cooperativeMultiDeviceLaunch is 1 if the device supports launching cooperative kernels via cudaLaunchCooperativeKernelMultiDevice, and 0 otherwise.
            pageableMemoryAccessUsesHostPageTables is 1 if the device accesses pageable memory via the host's page tables, and 0 otherwise.
            directManagedMemAccessFromHost is 1 if the host can directly access managed memory on the device without migration, and 0 otherwise.
            */
            return s;
        }
    }
}
